import React, { Component } from "react";
import HttpClient from "../../services/HttpClient";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { withRouter } from "react-router-dom";

class Login extends Component {
  state = {
    email: "",
    password: "",
    isRemembered: false,
    httpClient: new HttpClient("login/"),
  };

  render() {
    return (
      <div class="centered-content-wrap">
        <div class="centered-block">
          <h1>Login</h1>
          <ul>
            <li>
              <input
                type="text"
                name="email"
                value={this.state.email}
                onChange={this.handleChange}
                placeholder="Email"
                class="in-text large"
              />
            </li>
            <li>
              <input
                type="password"
                placeholder="Password"
                class="in-pass large"
                name="password"
                value={this.state.password}
                onChange={this.handleChange}
              />
            </li>
            <li class="last">
              <input
                type="checkbox"
                class="in-checkbox"
                id="remember"
                name="isRemembered"
                value={this.state.isRemembered}
                onChange={this.handleChange}
              />
              <label class="in-label" for="remember">
                Remember me
              </label>
              <span class="right">
                <a href="javascript:;" class="link">
                  Forgot password?
                </a>
                <a
                  href="javascript:;"
                  onClick={this.login.bind(this)}
                  class="btn orange"
                >
                  Login
                </a>
              </span>
            </li>
          </ul>
        </div>
      </div>
    );
  }

  async login() {
    const loginInfo = {
      email: this.state.email,
      password: this.state.password,
    };
    const response = await this.state.httpClient.create(loginInfo);
    debugger;
    if (response === undefined) {
      toast.configure();
      toast.error("Unsuccessful login!", {
        position: toast.POSITION.TOP_RIGHT,
      });
      return;
    }
    const token = response.token;
    const parts = response.token.split(".");
    const userInfo = JSON.parse(atob(parts[1]));

    this.state.isRemembered === true
      ? this.saveInLocalStorage(token, userInfo)
      : this.saveInSessionStorage(token, userInfo);
    this.props.history.push(`/`);
  }

  saveInLocalStorage(token, userInfo) {
    localStorage.setItem("storage", "local");
    localStorage.setItem("token", token);
    localStorage.setItem("id", userInfo.id);
    localStorage.setItem("role", userInfo.role);
    localStorage.setItem("name", userInfo.name);
  }

  saveInSessionStorage(token, userInfo) {
    debugger;
    localStorage.setItem("storage", "session");
    sessionStorage.setItem("token", token);
    sessionStorage.setItem("id", userInfo.id);
    sessionStorage.setItem("role", userInfo.role);
    sessionStorage.setItem("name", userInfo.name);
  }

  handleChange = (event) => {
    debugger;
    const { name, value, type, checked } = event.target;
    type === "checkbox"
      ? this.setState({
          [name]: checked,
        })
      : this.setState({
          [name]: value,
        });
  };
}

export default withRouter(Login);
