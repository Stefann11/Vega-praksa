import React, { Component } from "react";
import CalendarItem from "./CalendarItem";
import HttpClient from "../../services/HttpClient";

class Calendar extends Component {
  state = {
    date: new Date(),
    calendarItems: [],
    httpClient: new HttpClient("dailyTimeSheet/"),
    totalHours: 0,
  };

  async componentDidMount() {
    debugger;
    await this.loadData();
  }

  render() {
    const month = this.state.date.toLocaleString("default", {
      month: "long",
    });
    const year = this.state.date.getFullYear();
    const rows = this.getRows();

    const AllDates = () => rows.map((row) => <tr>{CalendarItemsList(row)}</tr>);

    const CalendarItemsList = (row) =>
      row.map((calendarItem) => (
        <CalendarItem key={calendarItem} calendarItem={calendarItem} />
      ));

    return (
      <section className="content">
        <h2>
          <i className="ico timesheet"></i>TimeSheet
        </h2>
        <div className="grey-box-wrap">
          <div className="top">
            <a
              href="javascript:;"
              className="prev"
              onClick={this.handleChangeMonth.bind(this)}
            >
              <i className="zmdi zmdi-chevron-left"></i>previous month
            </a>
            <span className="center">
              {month}, {year}
            </span>
            <a
              href="javascript:;"
              className="next"
              onClick={this.handleChangeMonth.bind(this)}
            >
              next month<i className="zmdi zmdi-chevron-right"></i>
            </a>
          </div>
          <div className="bottom"></div>
        </div>
        <table className="month-table">
          <tr className="head">
            <th>
              <span>monday</span>
            </th>
            <th>tuesday</th>
            <th>wednesday</th>
            <th>thursday</th>
            <th>friday</th>
            <th>saturday</th>
            <th>sunday</th>
          </tr>
          <tr className="mobile-head">
            <th>mon</th>
            <th>tue</th>
            <th>wed</th>
            <th>thu</th>
            <th>fri</th>
            <th>sat</th>
            <th>sun</th>
          </tr>
          <AllDates />
        </table>
        <div className="total">
          <span>
            Total hours: <em>{this.state.totalHours}</em>
          </span>
        </div>
      </section>
    );
  }

  async handleChangeMonth(month) {
    debugger;
    const date = this.state.date;
    const monthValue = month.target.innerText === "previous month" ? -1 : +1;
    const newDate = new Date(date.setMonth(date.getMonth() + monthValue));
    this.setState({
      date: newDate,
    });
    await this.loadData();
  }

  async loadData() {
    await this.loadCalendar();
    await this.loadTotalHours();
  }

  async loadTotalHours() {
    debugger;
    const totalHours = this.state.calendarItems.reduce(
      (previousHours, currentItem) => previousHours + currentItem.totalHours,
      0
    );
    this.setState({
      totalHours: totalHours,
    });
  }

  async loadCalendar() {
    const calendarItems = await this.state.httpClient.getWithQuery(
      "calendar?date=" + this.state.date.toISOString()
    );
    this.setState({ calendarItems: calendarItems });
  }

  getRows() {
    const numberOfRows = Math.floor(this.state.calendarItems.length / 7);
    const rows = [];
    Array.from(
      { length: numberOfRows },
      (x, i) => (rows[i] = this.state.calendarItems.slice(i * 7, (i + 1) * 7))
    );
    return rows;
  }
}

export default Calendar;
