import { useEffect, useState } from "react";
import axios from "axios";

type Recommendation = {
  articleId: string;
  article: string;
  recommendations: string[];
};

const RecommendationApp = () => {
  const [data, setData] = useState<Recommendation[]>([]);
  const [selectedId, setSelectedId] = useState<string>("");
  const [contentRecs, setContentRecs] = useState<string[]>([]);

  // Load collaborative recommendations
  useEffect(() => {
    axios
      .get<Recommendation[]>("http://localhost:5207/api/Recommendations/collaborative")
      .then((res) => {
        // Convert articleId to string explicitly
        const cleaned = res.data.map(item => ({
          ...item,
          articleId: String(item.articleId)
        }));
        setData(cleaned);
        if (cleaned.length > 0) {
          setSelectedId(cleaned[0].articleId);
        }
      })
      .catch((err) => console.error("API error:", err));
  }, []);

  // Load content-based recommendations on article selection
  useEffect(() => {
    if (selectedId) {
      axios
        .get<string[]>(`http://localhost:5207/api/Recommendations/content/${selectedId}`)
        .then((res) => setContentRecs(res.data))
        .catch((err) => {
          console.error("Content recs error:", err);
          setContentRecs([]); // fallback if error or none found
        });
    }
  }, [selectedId]);

  const selected = data.find((item) => item.articleId === selectedId);

  return (
    <div className="container mt-5">
      <h1 className="text-center mb-4">Article Recommendations</h1>

      <div className="mb-3">
        <label className="form-label fw-semibold">Select Article ID:</label>
        <select
          className="form-select"
          value={selectedId}
          onChange={(e) => setSelectedId(e.target.value)}
        >
          {data.map((item, index) => (
            <option key={`option-${item.articleId}-${index}`} value={item.articleId}>
              {item.articleId}
            </option>
          ))}
        </select>
      </div>

      {selected && (
        <>
          <h5 className="fw-bold mb-3">
            Recommendations for Article: <em>{selected.article}</em>
          </h5>

          <table className="table table-bordered text-center">
            <thead className="table-light">
              <tr>
                {[...Array(5)].map((_, idx) => (
                  <th key={`header-${idx}`}>Article {idx + 1}</th>
                ))}
              </tr>
            </thead>
            <tbody>
              <tr className="table-success">
                {selected.recommendations.map((rec, idx) => (
                  <td key={`collab-${idx}`}>
                    <strong>{rec}</strong>
                  </td>
                ))}
              </tr>
              <tr className="table-info">
                {[...contentRecs, ...Array(5 - contentRecs.length).fill("")].map((rec, idx) => (
                  <td key={`content-${idx}`}>{rec}</td>
                ))}
              </tr>
            </tbody>
          </table>

          <div className="mt-2">
            <span className="badge bg-success me-2">Collaborative Filtering</span>
            <span className="badge bg-info">Content Filtering</span>
          </div>
        </>
      )}
    </div>
  );
};

export default RecommendationApp;
