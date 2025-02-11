import { useState } from "react";
import MyInput from "./components/myInput.tsx";
import MyButton from "./components/myButton.tsx";
import MyText from "./components/myText.tsx";
import MyHeader from "./components/myHeader.tsx";

function App() {
  const [topic, setTopic] = useState<string>("");
  const [language, setLanguage] = useState<string>("English");
  const [audioSrc, setAudioSrc] = useState<string | null>(null);
  const [loading, setLoading] = useState<boolean>(false);

  const generatePodcast = async () => {
    setLoading(true); // Disable button while fetching
    try {
      const response = await fetch("http://localhost:5107/", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          topic: topic,
          language: "svenska",
          length: 5,
        }),
      });

      if (!response.ok) {
        throw new Error("Failed to generate podcast");
      }

      const blob = await response.blob(); // Convert response to a binary file
      const audioURL = URL.createObjectURL(blob);
      setAudioSrc(audioURL);
    } catch (error) {
      console.error("Error fetching the podcast:", error);
    } finally {
      setLoading(false); // Re-enable button after fetching
    }
  };

  return (
    <div className="d-flex flex-column justify-content-center align-items-center vh-100 w-100">
      <MyHeader />
      <MyText>
        ConversationalFriends is an AI-powered podcast generator. Just type in
        the topic you would like to listen to, along with the language you would
        like to listen in, and your favourite AI-podcast hosts Mira and Pierre
        will create a podcast episode tailored just for you!
      </MyText>
      <MyInput
        value={topic}
        placeholder="Enter a topic..."
        disabled={loading}
        type="text"
        onChange={(e) => setTopic(e.target.value)}
      />
      <MyInput
        value={language}
        placeholder="In which language?"
        disabled={loading}
        type="text"
        onChange={(e) => setLanguage(e.target.value)}
      />
      <MyButton
        className="btn-secondary"
        onClick={generatePodcast}
        disabled={loading}
      >
        {loading ? "Please wait" : "Generate podcast"}
      </MyButton>

      {audioSrc && <audio controls src={audioSrc} />}
    </div>
  );
}

export default App;
