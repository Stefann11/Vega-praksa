import React, { Component } from "react";
import NavigationBar from "./NavigationBar";
import { withRouter } from "react-router-dom";

class Header extends Component {
  state = {};
  render() {
    debugger;
    const name = sessionStorage.getItem("name") || localStorage.getItem("name");
    return (
      <header className="header">
        <div className="top-bar"></div>
        <div className="wrapper">
          <a href="javascript:;" className="logo">
            <img src="assets/img/logo.png" alt="VegaITSourcing Timesheet" />
          </a>
          <ul class="user right">
            <li>
              <a href="javascript:;">{name}</a>
              <div className="invisible" style={{ display: "none" }}></div>
              <div className="user-menu" style={{ display: "none" }}>
                <ul>
                  <li>
                    <a
                      onClick={this.resetPassword.bind(this)}
                      href="javascript:;"
                      className="link"
                    >
                      Change password
                    </a>
                  </li>
                  <li>
                    <a href="javascript:;" className="link">
                      Settings
                    </a>
                  </li>
                  <li>
                    <a href="javascript:;" className="link">
                      Export all data
                    </a>
                  </li>
                </ul>
              </div>
            </li>
            <li className="last">
              <a href="javascript:;" onClick={this.logout.bind(this)}>
                Logout
              </a>
            </li>
          </ul>
          <NavigationBar />
        </div>
      </header>
    );
  }

  resetPassword() {
    this.props.history.push(`/reset`);
  }

  logout() {
    this.removeLocalStorage();
    this.removeSessionStorage();
    this.props.history.push(`/login`);
  }

  removeLocalStorage() {
    localStorage.setItem("storage", "");
    localStorage.setItem("token", "");
    localStorage.setItem("id", "");
    localStorage.setItem("role", "");
    localStorage.setItem("name", "");
  }

  removeSessionStorage() {
    localStorage.setItem("storage", "");
    sessionStorage.setItem("token", "");
    sessionStorage.setItem("id", "");
    sessionStorage.setItem("role", "");
    sessionStorage.setItem("name", "");
  }
}

export default withRouter(Header);
