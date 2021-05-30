import React, { Component } from "react";
import HttpClient from "../../services/HttpClient";

class ProjectItem extends Component {
  state = {
    project: this.props.project,
    displayDetails: false,
    id: 0,
    name: "",
    description: "",
    employee: {},
    client: {},
    projectStatus: "0",
    httpClient: new HttpClient("project/"),
  };

  componentDidMount() {
    this.setState({
      id: this.state.project.id,
      name: this.state.project.name,
      description: this.state.project.description,
      employee: this.state.project.employee,
      client: this.state.project.client,
      projectStatus: this.state.project.projectStatus,
    });
  }

  handleChange = (event) => {
    debugger;
    const { name, value } = event.target;
    this.setState({
      [name]: value,
    });
  };

  render() {
    return (
      <div
        className={this.state.displayDetails === false ? "item" : "item open"}
      >
        <div className="heading" onClick={this.open.bind(this)}>
          <span>{this.state.name}</span>{" "}
          <span>
            <em>({this.state.description})</em>
          </span>
          <i>+</i>
        </div>
        <div
          className="details"
          style={{
            overflow: "hidden",
            display: this.state.displayDetails === false ? "none" : "block",
          }}
        >
          <ul className="form">
            <li>
              <label>Project name:</label>
              <input
                type="text"
                name="name"
                className="in-text"
                onChange={this.handleChange}
                value={this.state.name}
              />
            </li>
            <li>
              <label>Lead:</label>
              <select>
                <option>{this.state.employee.name}</option>
              </select>
            </li>
          </ul>
          <ul className="form">
            <li>
              <label>Description:</label>
              <input
                type="text"
                className="in-text"
                name="description"
                onChange={this.handleChange}
                value={this.state.description}
              />
            </li>
          </ul>
          <ul className="form last">
            <li>
              <label>Customer:</label>
              <select>
                <option>{this.state.client.name}</option>
              </select>
            </li>
            <li className="inline">
              <label>Status:</label>
              <span className="radio">
                <label for="inactive">Active:</label>
                <input
                  type="radio"
                  value="0"
                  name="projectStatus"
                  id="inactive"
                  onChange={this.handleChange}
                  checked={this.state.projectStatus === "0"}
                />
              </span>
              <span className="radio">
                <label for="active">Inactive:</label>
                <input
                  type="radio"
                  value={1}
                  name="projectStatus"
                  id="active"
                  onChange={this.handleChange}
                  checked={this.state.projectStatus === 1}
                />
              </span>
              <span className="radio">
                <label for="active">Archive:</label>
                <input
                  type="radio"
                  value="2"
                  name="projectStatus"
                  id="active"
                  onChange={this.handleChange}
                  checked={this.state.projectStatus === "2"}
                />
              </span>
            </li>
          </ul>
          <div className="buttons">
            <div className="inner">
              <a href="" onClick={this.edit.bind(this)} className="btn green">
                Save
              </a>
              <a href="" onClick={this.delete.bind(this)} className="btn red">
                Delete
              </a>
            </div>
          </div>
        </div>
      </div>
    );
  }

  async edit() {
    debugger;
    const project = {
      id: this.state.id,
      name: this.state.name,
      description: this.state.description,
      projectStatus: this.state.projectStatus,
      employee: this.state.employee,
      client: this.state.client,
    };
    await this.state.httpClient.edit(project);
  }

  async delete() {
    debugger;
    const project = {
      id: this.state.id,
      name: this.state.name,
      description: this.state.description,
      projectStatus: this.state.projectStatus,
      employee: this.state.employee,
      client: this.state.client,
    };
    await this.state.httpClient.delete(project);
  }

  open = () => {
    debugger;
    this.setState((prevState) => {
      return {
        ...prevState,
        displayDetails: !prevState.displayDetails,
      };
    });
  };
}

export default ProjectItem;
