import React, { Component } from "react";
import Layout from "../layouts/Layout";
import Calendar from "../components/DailyTimeSheet/Calendar";
import { withRouter } from "react-router-dom";

class DailyTimeSheetPage extends Component {
  render() {
    return (
      <Layout>
        <Calendar />
      </Layout>
    );
  }
}

export default withRouter(DailyTimeSheetPage);
