import React, { Component } from "react";
import { Modal, ModalHeader, ModalBody, ModalFooter } from "reactstrap";
import HttpClient from "../../services/HttpClient";
import "../../css/app.css";

class CreateNewProjectModal extends Component {
  state = {
    showCreateNewProjectModal: this.props.show,
    name: "",
    description: "",
    projectStatus: 1,
    employee: {},
    client: {},
    employees: [],
    clients: [],
  };

  async componentDidMount() {
    const httpClientEmployees = new HttpClient("employee/");
    const employees = await httpClientEmployees.getAll();
    this.setState({ employees: employees });

    const httpClientClients = new HttpClient("client/");
    const clients = await httpClientClients.getAll();
    this.setState({ clients: clients });
  }

  render() {
    debugger;
    return (
      <Modal isOpen={this.state.showCreateNewProjectModal} centered={true}>
        <ModalHeader toggle={this.toggle.bind(this)}></ModalHeader>
        <ModalBody>
          <div id="new-member" className="new-member-inner">
            <h2>Create new project</h2>
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
                <label>Description:</label>
                <input
                  type="text"
                  className="in-text"
                  name="description"
                  onChange={this.handleChange}
                  value={this.state.description}
                />
              </li>
              <li>
                <label>Customer:</label>
                <select onChange={this.handleChangeClient}>
                  <option>Select customer</option>
                  {this.state.clients.map((option, index) => (
                    <option key={index} value={index}>
                      {option.name}
                    </option>
                  ))}
                </select>
              </li>
              <li>
                <label>Lead:</label>
                <select onChange={this.handleChangeEmployee}>
                  <option>Select lead</option>
                  {this.state.employees.map((option, index) => (
                    <option key={index} value={index}>
                      {option.name}
                    </option>
                  ))}
                </select>
              </li>
            </ul>
          </div>
        </ModalBody>
        <ModalFooter>
          <div className="buttons">
            <div className="inner">
              <a href="" onClick={this.create.bind(this)} className="btn green">
                Save
              </a>
            </div>
          </div>
        </ModalFooter>
      </Modal>
    );
  }

  toggle() {
    debugger;
    this.setState({ showCreateNewProjectModal: false });
    this.props.onShowChange();
  }

  async create() {
    debugger;
    const project = {
      name: this.state.name,
      description: this.state.description,
      projectStatus: this.state.projectStatus,
      employee: this.state.employee,
      client: this.state.client,
    };
    const httpClient = new HttpClient("project/");
    await httpClient.create(project);
  }

  handleChange = (event) => {
    const { name, value } = event.target;
    this.setState({
      [name]: value,
    });
  };

  handleChangeEmployee = (e) => {
    this.setState({ employee: this.state.employees[e.target.value] });
  };

  handleChangeClient = (e) => {
    this.setState({ client: this.state.clients[e.target.value] });
  };
}

export default CreateNewProjectModal;
