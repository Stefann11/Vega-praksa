import React, { Component } from "react";
import HttpClient from "../../services/HttpClient";
import { withRouter } from "react-router-dom";

class ResetPassword extends Component {
  state = {
    email: "",
    httpClient: new HttpClient("login/"),
  };

  render() {
    return (
      <div className="centered-content-wrap">
        <div className="centered-block">
          <h1>reset password</h1>
          <ul>
            <li>
              <input
                type="text"
                placeholder="Email"
                className="in-text large"
              />
            </li>
            <li className="right">
              <a href="javascript:;" className="btn orange">
                Reset password
              </a>
            </li>
          </ul>
        </div>
      </div>
    );
  }
}

export default withRouter(ResetPassword);
