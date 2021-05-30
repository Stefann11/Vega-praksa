import React, { Component } from "react";
import moment from "moment";

class ReportItem extends Component {
  state = {
    dailyTimeSheet: this.props.dailyTimeSheet,
  };

  render() {
    return (
      <tr>
        <td>{moment(this.state.dailyTimeSheet.date).format("DD/MM/YYYY")}</td>
        <td>{this.state.dailyTimeSheet.employee.name}</td>
        <td>{this.state.dailyTimeSheet.project.name}</td>
        <td>{this.state.dailyTimeSheet.category.name}</td>
        <td>{this.state.dailyTimeSheet.description}</td>
        <td class="small">
          {this.state.dailyTimeSheet.time + this.state.dailyTimeSheet.overtime}
        </td>
      </tr>
    );
  }
}

export default ReportItem;
