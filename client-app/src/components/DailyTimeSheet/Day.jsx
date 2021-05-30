import React, { Component } from "react";
import DayItem from "./DayItem";
import HttpClient from "../../services/HttpClient";

class Day extends Component {
  state = {
    date: Date.parse(this.props.date),
    dayItems: [],
    httpClient: new HttpClient("dailyTimeSheet/"),
    totalHours: 0,
  };

  async componentDidMount() {
    debugger;
    await this.loadData();
  }

  render() {
    debugger;
    const dateString = this.generateDateString();
    const days = this.generateDays();
    debugger;

    const DayItemsList = () =>
      this.state.dayItems.map((dayItem) => (
        <DayItem
          concatNewDayItem={this.concatNewDayItem.bind(this)}
          key={dayItem}
          dayItem={dayItem}
          date={this.state.date}
        />
      ));

    const Days = () =>
      days.map((day, i) => (
        <li
          onClick={() => this.handleChangeDay(day)}
          className={this.checkClassNameActive(day)}
        >
          <a href="javascript:;">
            <b>{this.generateWeeklyDateNames(day)}</b>
            <i>7.5</i>
            <span>{this.generateDayName(i)}</span>
          </a>
        </li>
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
              onClick={this.handleChangeWeek.bind(this)}
              className="prev"
            >
              <i className="zmdi zmdi-chevron-left"></i>previous week
            </a>
            <span className="center">{dateString}</span>
            <a
              href="javascript:;"
              onClick={this.handleChangeWeek.bind(this)}
              className="next"
            >
              next week<i className="zmdi zmdi-chevron-right"></i>
            </a>
          </div>
          <div className="bottom">
            <ul className="days">
              <Days />
            </ul>
          </div>
        </div>
        <table className="default-table">
          <tr>
            <th>
              Client <em>*</em>
            </th>
            <th>
              Project <em>*</em>
            </th>
            <th>
              Category <em>*</em>
            </th>
            <th>Description</th>
            <th className="small">
              Time <em>*</em>
            </th>
            <th className="small">Overtime</th>
          </tr>
          <DayItemsList />
          <DayItem
            concatNewDayItem={this.concatNewDayItem.bind(this)}
            dayItem={null}
            date={this.state.date}
          />
        </table>
        <div className="total">
          <a href="/">
            <i></i>back to monthly view
          </a>
          <span>
            Total hours: <em>{this.state.totalHours}</em>
          </span>
        </div>
      </section>
    );
  }

  async handleChangeWeek(days) {
    debugger;
    const date = new Date(this.state.date.valueOf());
    const dayValue = days.target.innerText === "previous week" ? -7 : 7;
    const newDate = new Date(date.setDate(date.getDate() + dayValue));
    await this.setState({
      date: newDate,
    });
    await this.loadData();
  }

  checkClassNameActive(day) {
    debugger;
    return new Date(day).getTime() == new Date(this.state.date).getTime()
      ? "active"
      : "";
  }

  async handleChangeDay(day) {
    debugger;
    const date = Date.parse(day);
    const newDate = new Date(date.valueOf());
    await this.setState({
      date: newDate,
    });
    await this.loadData();
  }

  generateDayName(i) {
    const dayNames = [
      "monday",
      "tuesday",
      "wednesday",
      "thursday",
      "friday",
      "saturday",
      "sunday",
    ];
    return dayNames[i];
  }

  generateDays() {
    const week = this.getWeek();
    let weekStart = week.weekStart;
    const weekEnd = week.weekEnd;
    const days = [];

    while (weekStart <= weekEnd) {
      days.push(new Date(weekStart).toString());
      weekStart = new Date(
        new Date(weekStart).setDate(weekStart.getDate() + 1)
      );
    }
    return days;
  }

  generateWeeklyDateNames(dateString) {
    const date = new Date(dateString);
    const day = date.getDate();
    const month = date.toLocaleString("default", {
      month: "long",
    });
    return day + " " + month;
  }

  generateDateString() {
    const week = this.getWeek();
    const weekStart = week.weekStart;
    const weekEnd = week.weekEnd;
    const startMonth = weekStart.toLocaleString("default", {
      month: "long",
    });
    const year = weekStart.getFullYear();
    const startDay = weekStart.getDate();

    const endMonth = weekEnd.toLocaleString("default", {
      month: "long",
    });
    const endDay = weekEnd.getDate();

    return (
      startMonth +
      " " +
      startDay +
      " - " +
      endMonth +
      " " +
      endDay +
      ", " +
      year
    );
  }

  getWeek() {
    const dt = new Date(this.state.date.valueOf());
    const currentWeekDay = dt.getDay();
    const lessDays = currentWeekDay == 0 ? 6 : currentWeekDay - 1;
    const weekStart = new Date(new Date(dt).setDate(dt.getDate() - lessDays));
    const weekEnd = new Date(
      new Date(weekStart).setDate(weekStart.getDate() + 6)
    );
    return { weekStart, weekEnd };
  }

  async concatNewDayItem() {
    let dayItems = this.state.dayItems;
    const dailyTimeSheet = null;
    dayItems = await dayItems.concat(dailyTimeSheet);
    await this.setState({
      dayItems: dayItems,
    });
    debugger;
    console.log(this.state.dayItems);
  }

  async loadData() {
    await this.loadDay();
    await this.loadTotalHours();
  }

  async loadTotalHours() {
    debugger;
    const totalHours = this.state.dayItems.reduce(
      (previousHours, currentItem) =>
        previousHours + currentItem.time + currentItem.overtime,
      0
    );
    this.setState({
      totalHours: totalHours,
    });
  }

  async loadDay() {
    const x = new Date().getTimezoneOffset() * 60000;
    const localISOTime = new Date(new Date(this.state.date) - x)
      .toISOString()
      .slice(0, -1);
    const dayItems = await this.state.httpClient.getWithQuery(
      "day?date=" + localISOTime
    );
    this.setState({ dayItems: dayItems });
  }
}

export default Day;
