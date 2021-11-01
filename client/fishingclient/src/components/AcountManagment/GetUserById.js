import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import "./Login.css";

const GetUserById = () => {
  const history = useHistory();
  const [userId, setUserId] = useState("");
  const [user, setUser] = useState([]);
  const [redirect, setRedirect] = useState(false);

   
  const submit = (e) => {
    e.preventDefault();

    const myFunc = (() => {
        (async () => {
          const response = await fetch("https://localhost:44366/api/AppUsers/getUser/id?userId=" + userId,
          )
          const content = await response.json();
          setUser(content);
          setRedirect(true);
        })()
      });

      redirect();

       
      myFunc();
      // fetch("https://localhost:44343/api/AppUsers/getUser/id?userId=" + userId)
      //   .then((r) => r.json())
      //   .then((res) => setUser(res));
      
      // console.log(user);
      
      // history.push("/UserDetails", { data: user });
      setRedirect(true);
      history.push("/UserDetails", { data: user });
  };
  //   if (redirect) {
  //     return <Redirect to="/UserDetails" props={user} />;
  //   }
  return (
    <main className="form-signin">
      <form onSubmit={submit}>
        <h1 className="h3 mb-3 fw-normal">Please enter user id</h1>

        <div className="form-floating">
          <input
            type="text"
            className="form-control"
            onChange={(e) => setUserId(e.target.value)}
          />
          <label for="floatingInput">User Id</label>
        </div>

        <button className="w-100 btn btn-lg btn-primary" type="submit">
          Submit
        </button>
      </form>
    </main>
  );
};

export default GetUserById;
