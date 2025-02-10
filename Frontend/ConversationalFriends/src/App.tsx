import { useState } from "react";

function App() {
  const [topic, setTopic] = useState<string>("");
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
          length: 2,
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
    <div className="container-sm text-center">
      <h1>Conversational Friends</h1>
      <p className="text-start">
        Sick of listening to the same old boring podcasts over and over?
        <br />
        Well, you're in luck! Conversational Friends is an AI-powered podcast
        generator that creates a podcast with a topic of YOUR choice!
        <br />
        Just type in a topic and let the AI do the rest!
      </p>
      <input
        type="text"
        className="form-control"
        placeholder="Enter a topic..."
        value={topic}
        onChange={(e) => setTopic(e.target.value)}
        disabled={loading} // Disable input while loading
      />
      <button
        className="btn btn-secondary mt-2"
        onClick={generatePodcast}
        disabled={loading}
      >
        {loading ? "Please wait..." : "Generate Podcast"}
      </button>
      <br />
      <br />
      {audioSrc && <audio controls src={audioSrc} />}
    </div>
  );
}

export default App;
