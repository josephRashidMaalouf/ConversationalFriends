import { useState } from "react";
import MyInput from "./components/myInput.tsx";
import MyButton from "./components/myButton.tsx";
import MyText from "./components/myText.tsx";
import MyCard from "./components/myCard.tsx";
import MyAvatar from "./components/myAvatar.tsx";
import "bootstrap-icons/font/bootstrap-icons.css";

function App() {
  const [topic, setTopic] = useState<string>("");
  const [language, setLanguage] = useState<string>("English");
  const [audioSrc, setAudioSrc] = useState<string | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [apiUrl] = useState<string>(
    window._env_?.API_URL || "http://localhost:5000"
  );

  const generatePodcast = async () => {
    setLoading(true); // Disable button while fetching
    try {
      const response = await fetch(`${apiUrl}`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          topic: topic,
          language: language,
          length: 4,
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
      <MyCard>
        <MyText header="Conversational Friends"></MyText>
        <div className="container mt-3 mb-5">
          <div className="d-flex justify-content-evenly">
            <MyAvatar
              src="mira-avatar.webp"
              alt="Mira"
              size={80}
              avatarName="Mira"
            />
            <MyAvatar
              src="pierre-avatar.webp"
              alt="Pierre"
              size={80}
              avatarName="Pierre"
            />
          </div>
        </div>
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
          className="btn-brown"
          onClick={generatePodcast}
          disabled={loading}
        >
          <i className="bi bi-mic-fill"></i>
          {loading ? "Please wait" : "Generate podcast"}
        </MyButton>

        <div className="mt-3 mb-3">
          {audioSrc && <audio controls src={audioSrc} />}
        </div>
      </MyCard>
    </div>
  );
}

export default App;
