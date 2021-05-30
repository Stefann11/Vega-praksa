import React, { Component } from "react";
import Footer from "../components/Common/Footer";
import Header from "../components/Common/Header";

class Layout extends Component {
  render() {
    return (
      <div className="container">
        <Header />
        <div className="wrapper">
          {this.props.children}
          <Footer />
        </div>
      </div>
    );
  }
}

export default Layout;
