import React, { Component } from "react";

class LoginLayout extends Component {
  render() {
    return (
      <div class="wrapper centered">
        <div class="logo-wrap">
          <a href="index.html" class="inner">
            <img src="assets/img/logo-large.png" />
          </a>
        </div>
        {this.props.children}
      </div>
    );
  }
}

export default LoginLayout;
