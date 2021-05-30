import React, { Component } from "react";
import Projects from "../components/Project/Projects";
import Layout from "../layouts/Layout";
import { withRouter } from "react-router-dom";

class ProjectPage extends Component {
  render() {
    return (
      <Layout>
        <Projects />
      </Layout>
    );
  }
}

export default withRouter(ProjectPage);
