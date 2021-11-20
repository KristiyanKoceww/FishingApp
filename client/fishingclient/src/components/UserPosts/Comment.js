import { Col, Container, Row } from "react-bootstrap";
import "./styles/comment.scss";

const Comment = (props) => {
  const { accountName, comment, key } = props;

  const edit = () => {
    console.log(null);
    return null;
  };

  const deleteComment = (commentId) => {
    const jwt = localStorage.getItem("jwt");
    if (window.confirm("Are you sure you want to remove comment?")) {
      fetch("https://localhost:44366/api/Comments/delete", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + jwt,
        },
        body: commentId,
      });
    }
  };

  return (
    <div className="commentContainer">
      <div className="accountName">{accountName}</div>
      <div className="comment">
        {comment}
        <div className="grid-container">
          <Container>
            <div>
              <Row xs="auto">
                <Col>
                  {" "}
                  <button className="button" onClick={edit}>
                    {" "}
                    Отговор
                  </button>
                </Col>
                <Col>
                  <button className="button" onClick={edit}>
                    {" "}
                    Редактирай
                  </button>
                </Col>
                <Col>
                  {" "}
                  <button
                    className="button"
                    onClick={() => deleteComment(comment.Id)}
                  >
                    {" "}
                    Изтрий
                  </button>
                </Col>
              </Row>
            </div>
          </Container>
        </div>
      </div>
    </div>
  );
};

export default Comment;
