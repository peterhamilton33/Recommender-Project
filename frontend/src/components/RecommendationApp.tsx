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

  useEffect(() => {
    axios
      .get<Recommendation[]>("http://localhost:5207/api/Recommendations/collaborative")
      .then((res) => {
        setData(res.data);
        if (res.data.length > 0) {
          setSelectedId(res.data[0].articleId);
        }
      })
      .catch((err) => console.error("API error:", err));
  }, []);

  const selected = data.find((item) => item.articleId === selectedId);

  return (
    <div className="container mt-5">
      <h1 className="text-center mb-4">Collaborative Recommendations</h1>

      <div className="mb-3">
        <label className="form-label fw-semibold">Select Article ID:</label>
        <select
          className="form-select"
          value={selectedId}
          onChange={(e) => setSelectedId(e.target.value)}
        >
          {data.map((item) => (
            <option key={item.articleId} value={item.articleId}>
              {item.articleId}
            </option>
          ))}
        </select>
      </div>

      {selected && (
        <>
          <h5 className="fw-bold mb-3">
            Recommendations for Article:{" "}
            <em>{selected.article}</em>
          </h5>

          <table className="table table-bordered text-center">
            <thead className="table-light">
              <tr>
                {selected.recommendations.map((_, idx) => (
                  <th key={idx}>Article {idx + 1}</th>
                ))}
              </tr>
            </thead>
            <tbody>
              <tr>
                {selected.recommendations.map((rec, idx) => (
                  <td key={idx}>{rec}</td>
                ))}
              </tr>
            </tbody>
          </table>
        </>
      )}
    </div>
  );
};

export default RecommendationApp;
