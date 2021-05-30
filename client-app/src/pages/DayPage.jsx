import React, { Component } from "react";
import Layout from "../layouts/Layout";
import Day from "../components/DailyTimeSheet/Day";

class DayPage extends Component {
  render() {
    debugger;
    return (
      <Layout>
        <Day date={this.props.match.params.date} />
      </Layout>
    );
  }
}

export default DayPage;
