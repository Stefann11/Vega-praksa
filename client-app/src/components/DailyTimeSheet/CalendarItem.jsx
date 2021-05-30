import React, { Component } from "react";
import moment from "moment";
import { withRouter } from "react-router-dom";

class CalendarItem extends Component {
  state = {
    calendarItem: this.props.calendarItem,
  };

  render() {
    const style = this.getClassName();
    return (
      <td className={style}>
        <div className="date">
          <span>{moment(this.state.calendarItem.date).format("DD")}.</span>
        </div>
        <div className="hours">
          <a onClick={this.handleClick} a href="javascript:;">
            Hours: <span>{this.state.calendarItem.totalHours}</span>
          </a>
        </div>
      </td>
    );
  }

  handleClick = () => {
    debugger;
    this.props.history.push(`/day/${this.state.calendarItem.date}`);
  };

  getClassName() {
    const date = new Date(this.state.calendarItem.date);
    const dateNow = new Date();
    const firstDay = new Date(dateNow.getFullYear(), dateNow.getMonth(), 1);

    const previous = date.getTime() < firstDay.getTime() ? "previous" : "";

    const positivity =
      this.state.calendarItem.totalHours >= 7
        ? "positive"
        : this.state.calendarItem.totalHours === 0
        ? ""
        : "negative";

    return date.getTime() >= dateNow.getTime()
      ? "disable"
      : `${positivity} ${previous}`;
  }
}

export default withRouter(CalendarItem);
