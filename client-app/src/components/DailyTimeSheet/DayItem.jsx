import React, { Component } from "react";
import HttpClient from "../../services/HttpClient";

class DayItem extends Component {
  state = {
    dayItem: {
      id: 0,
      project: {
        client: {},
      },
      category: {},
      description: "",
      time: 0,
      overtime: 0,
    },
    client: { id: 1 },
    date: this.props.date,
    clients: [],
    project: [],
    category: [],
  };

  async componentDidMount() {
    debugger;
    if (this.props.dayItem !== null)
      this.setState({
        dayItem: this.props.dayItem,
        client: this.props.dayItem.project.client,
      });

    const httpClientClients = new HttpClient("client/");
    const clients = await httpClientClients.getAll();
    this.setState({ clients: clients });

    await this.loadProjects();

    const httpClientCategories = new HttpClient("category/");
    const categories = await httpClientCategories.getAll();
    this.setState({ category: categories });
  }

  render() {
    debugger;
    return (
      <tr>
        <td>
          <select onChange={(e) => this.handleChangeSelectClient(e)}>
            <option>{this.state.client.name}</option>
            {this.state.clients.map((option, index) => (
              <option key={index} value={index}>
                {option.name}
              </option>
            ))}
          </select>
        </td>
        <td>
          <select onChange={(e) => this.handleChangeSelect(e, "project")}>
            <option>{this.state.dayItem.project.name}</option>
            {this.state.project.map((option, index) => (
              <option key={index} value={index}>
                {option.name}
              </option>
            ))}
          </select>
        </td>
        <td>
          <select onChange={(e) => this.handleChangeSelect(e, "category")}>
            <option>{this.state.dayItem.category.name}</option>
            {this.state.category.map((option, index) => (
              <option key={index} value={index}>
                {option.name}
              </option>
            ))}
          </select>
        </td>
        <td>
          <input
            type="text"
            name="description"
            value={this.state.dayItem.description}
            className="in-text medium"
            onChange={this.handleChangeDayItem.bind(this)}
          />
        </td>
        <td className="small">
          <input
            type="number"
            name="time"
            value={this.state.dayItem.time}
            onChange={this.handleChangeDayItem.bind(this)}
            className="in-text xsmall"
          />
        </td>
        <td className="small">
          <input
            type="number"
            name="overtime"
            value={this.state.dayItem.overtime}
            onChange={this.handleChangeDayItem.bind(this)}
            className="in-text xsmall"
          />
        </td>
      </tr>
    );
  }

  async handleChangeDayItem(event) {
    debugger;
    const { name, value } = event.target;
    await this.setState((prevState) => ({
      dayItem: {
        ...prevState.dayItem,
        [name]: value,
      },
    }));
    await this.createOrEdit();
  }

  async handleChangeSelect(e, prop) {
    debugger;
    const dayItem = this.state.dayItem;
    dayItem[prop] = this.state[prop][e.target.value];
    await this.setState({ dayItem: dayItem });
    await this.createOrEdit();
  }

  async loadProjects() {
    debugger;
    const httpClientProjects = new HttpClient(
      "project/byClient/" + this.state.client.id
    );
    const projects = await httpClientProjects.getAll();
    this.setState({ project: projects });
  }

  async handleChangeSelectClient(e) {
    debugger;
    await this.setState({ client: this.state.clients[e.target.value] });
    await this.loadProjects();
    await this.createOrEdit();
  }

  async createOrEdit() {
    this.state.dayItem.id === 0
      ? await this.createDailyTimeSheet()
      : await this.editDailyTimeSheet();
  }

  async editDailyTimeSheet() {
    debugger;
    console.log(this.state.dayItem);
    const httpClient = new HttpClient("dailyTimeSheet/");
    await httpClient.edit(this.state.dayItem);
  }

  async createDailyTimeSheet() {
    debugger;
    const date = new Date(this.state.date);
    date.setTime(date.getTime() + 1 * 60 * 60 * 1000);
    if (this.checkValidDailyTimeSheet()) {
      const dailyTimeSheet = {
        date: date,
        description: this.state.dayItem.description,
        time: this.state.dayItem.time,
        overtime: this.state.dayItem.overtime,
        project: this.state.dayItem.project,
        category: this.state.dayItem.category,
        employee: this.state.dayItem.project.employee,
      };
      const httpClient = new HttpClient("dailyTimeSheet/");
      const response = await httpClient.create(dailyTimeSheet);
      await this.props.concatNewDayItem(response);
    }
  }

  checkValidDailyTimeSheet() {
    return (
      this.state.dayItem.project.client !== {} &&
      this.state.dayItem.project !== {} &&
      this.state.dayItem.category !== {} &&
      this.state.dayItem.time !== 0
    );
  }
}

export default DayItem;
