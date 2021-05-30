import React, { Component } from "react";
import HttpClient from "../../services/HttpClient";
import ReportItem from "./ReportItem";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

class Reports extends Component {
  state = {
    dailyTimeSheets: [],
    totalHours: 0,
    all: {
      client: [],
      employee: [],
      project: [],
      category: [],
    },
    client: {},
    employee: {},
    project: {},
    category: {},
    startDate: "",
    endDate: "",
  };

  async componentDidMount() {
    const httpClientClients = new HttpClient("client/");
    const clients = await httpClientClients.getAll();
    const all = this.state.all;
    all.client = clients;
    this.setState({ all: all });

    const httpClientEmployees = new HttpClient("employee/");
    const teamMembers = await httpClientEmployees.getAll();
    all.employee = teamMembers;
    this.setState({ all: all });

    const httpClientProjects = new HttpClient("project/");
    const projects = await httpClientProjects.getAll();
    all.project = projects;
    this.setState({ all: all });

    const httpClienCategories = new HttpClient("category/");
    const categories = await httpClienCategories.getAll();
    all.category = categories;
    this.setState({ all: all });
  }

  render() {
    const ReportsList = () =>
      this.state.dailyTimeSheets.map((dailyTimeSheet) => (
        <ReportItem key={dailyTimeSheet.id} dailyTimeSheet={dailyTimeSheet} />
      ));
    return (
      <section class="content">
        <h2>
          <i class="ico report"></i>Reports
        </h2>
        <div class="grey-box-wrap reports">
          <ul class="form">
            <li>
              <label>Team member:</label>
              <select
                disabled={this.checkLoggedRole() === "Worker"}
                value={this.getValue("employee")}
                onChange={(e) => this.handleChangeSelect(e, "employee")}
              >
                <option>All</option>
                {this.state.all.employee.map((option, index) => (
                  <option key={index} value={index}>
                    {option.name}
                  </option>
                ))}
              </select>
            </li>
            <li>
              <label>Category:</label>
              <select
                value={this.getValue("category")}
                onChange={(e) => this.handleChangeSelect(e, "category")}
              >
                <option>All</option>
                {this.state.all.category.map((option, index) => (
                  <option key={index} value={index}>
                    {option.name}
                  </option>
                ))}
              </select>
            </li>
          </ul>
          <ul class="form">
            <li>
              <label>Client:</label>
              <select
                value={this.getValue("client")}
                onChange={(e) => this.handleChangeSelect(e, "client")}
              >
                <option>All</option>
                {this.state.all.client.map((option, index) => (
                  <option key={index} value={index}>
                    {option.name}
                  </option>
                ))}
              </select>
            </li>
            <li>
              <label>Start date:</label>
              <DatePicker
                className="in-text datepicker"
                name="startDate"
                dateFormat="dd/MM/yyyy"
                selected={this.state.startDate}
                maxDate={new Date()}
                onChange={(e) => this.handleChangeDate(e, "startDate")}
              />
            </li>
          </ul>
          <ul class="form last">
            <li>
              <label>Project:</label>
              <select
                value={this.getValue("project")}
                onChange={(e) => this.handleChangeSelect(e, "project")}
              >
                <option>All</option>
                {this.state.all.project.map((option, index) => (
                  <option key={index} value={index}>
                    {option.name}
                  </option>
                ))}
              </select>
            </li>
            <li>
              <label>End date:</label>
              <DatePicker
                className="in-text datepicker"
                name="endDate"
                dateFormat="dd/MM/yyyy"
                selected={this.state.endDate}
                maxDate={new Date()}
                onChange={(e) => this.handleChangeDate(e, "endDate")}
              />
            </li>
            <li>
              <a
                onClick={() => this.reset()}
                href="javascript:;"
                class="btn orange right"
              >
                Reset
              </a>
              <a
                onClick={() => this.search()}
                href="javascript:;"
                class="btn green right"
              >
                Search
              </a>
            </li>
          </ul>
        </div>
        <table class="default-table">
          <tr>
            <th>Date</th>
            <th>Team member</th>
            <th>Projects</th>
            <th>Categories</th>
            <th>Description</th>
            <th class="small">Time</th>
          </tr>
          <ReportsList />
        </table>
        <div class="total">
          <span>
            Report total: <em>{this.state.totalHours}</em>
          </span>
        </div>
        <div class="grey-box-wrap reports">
          <div class="btns-inner">
            <a
              onClick={() => this.printPdf()}
              href="javascript:;"
              class="btn white"
            >
              <span>Print report</span>
            </a>
            <a
              onClick={() => this.generatePdf()}
              href="javascript:;"
              class="btn white"
            >
              <span>Create PDF</span>
            </a>
            <a
              onClick={() => this.generateExcel()}
              href="javascript:;"
              class="btn white"
            >
              <span>Export to excel</span>
            </a>
          </div>
        </div>
      </section>
    );
  }

  handleChangeDate = (event, prop) => {
    debugger;
    this.setState({
      [prop]: event,
    });
  };

  handleChangeSelect(e, prop) {
    debugger;
    const state = this.state;
    state[prop] = this.state.all[prop][e.target.value];
    this.setState({ [prop]: state[prop] });
  }

  async generatePdf() {
    const httpClient = new HttpClient("report/pdf/");
    await httpClient.download(this.state.dailyTimeSheets, "report.pdf");
  }

  async printPdf() {
    const httpClient = new HttpClient("report/print/");
    await httpClient.getAll(this.state.dailyTimeSheets);
  }

  getValue(prop) {
    return !this.state[prop]
      ? "All"
      : this.state.all[prop][this.state[prop].name];
  }

  async search() {
    const parameter = {
      employee: this.getParameterValue(this.state.employee),
      client: this.getParameterValue(this.state.client),
      project: this.getParameterValue(this.state.project),
      category: this.getParameterValue(this.state.category),
      startDate: this.state.startDate === null ? "" : this.state.startDate,
      endDate: this.state.endDate === null ? "" : this.state.endDate,
    };
    const httpClient = new HttpClient("dailyTimeSheet/");
    const dailyTimeSheets = await httpClient.getWithQuery(
      this.checkSearchRole() +
        "?employee=" +
        parameter.employee +
        "&client=" +
        parameter.client +
        "&project=" +
        parameter.project +
        "&category=" +
        parameter.category +
        "&startDate=" +
        parameter.startDate +
        "&endDate=" +
        parameter.endDate
    );
    debugger;
    this.setState({ dailyTimeSheets: dailyTimeSheets });
    this.loadTotalHours();
  }

  checkLoggedRole() {
    const admin =
      sessionStorage.getItem("role") === "Admin" ||
      localStorage.getItem("role") === "Admin";
    return admin ? "Admin" : "Worker";
  }

  checkSearchRole() {
    return this.checkLoggedRole() === "Admin" ? "search" : "searchEmployee";
  }

  getParameterValue(object) {
    return this.checkUndefined(object) ? "" : object.id;
  }

  checkUndefined(object) {
    return object === undefined || object.id === undefined;
  }

  async generateExcel() {
    const httpClient = new HttpClient("report/excel/");
    await httpClient.download(this.state.dailyTimeSheets, "report.xlxc");
  }

  async reset() {
    this.setState({
      startDate: "",
      endDate: "",
      client: {},
      employee: {},
      project: {},
      category: {},
    });
    this.setState({ dailyTimeSheets: [], totalHours: 0 });
  }

  async loadData() {
    await this.loadReports();
    await this.loadTotalHours();
  }

  async loadTotalHours() {
    debugger;
    const totalHours = this.state.dailyTimeSheets.reduce(
      (previousHours, currentItem) =>
        previousHours + currentItem.time + currentItem.overtime,
      0
    );
    this.setState({
      totalHours: totalHours,
    });
  }

  async loadReports() {
    const httpClientDailyTimeSheet = new HttpClient("dailyTimeSheet/");
    const dailyTimeSheets = await httpClientDailyTimeSheet.getAll();
    this.setState({ dailyTimeSheets: dailyTimeSheets });
  }
}

export default Reports;
