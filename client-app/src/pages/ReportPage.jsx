import React, { Component } from "react";
import Reports from "../components/Report/Reports";
import Layout from "../layouts/Layout";
import { withRouter } from "react-router-dom";

class ReportPage extends Component {
  render() {
    return (
      <Layout>
        <Reports />
      </Layout>
    );
  }
}

export default withRouter(ReportPage);
