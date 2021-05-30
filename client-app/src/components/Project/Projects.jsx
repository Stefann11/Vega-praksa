import React, { Component } from "react";
import Pagination from "../Common/Pagination";
import LettersMenu from "../Common/LettersMenu";
import ProjectItem from "./ProjectItem";
import SearchElement from "./SearchElement";
import HttpClient from "../../services/HttpClient";

class Projects extends Component {
  state = {
    projects: [],
    name: "",
    page: 1,
    letter: "",
    pages: 1,
    httpClient: new HttpClient("project/"),
  };

  async componentDidMount() {
    debugger;
    this.loadProjects();
  }

  render() {
    const ProjectList = () =>
      this.state.projects.map((project) => (
        <ProjectItem key={project.id} project={project} />
      ));
    return (
      <section className="content">
        <h2>
          <i className="ico projects"></i>Projects
        </h2>
        <SearchElement
          name={this.state.name}
          onChange={this.handleChange.bind(this)}
        />
        <LettersMenu
          letter={this.state.letter}
          onClick={() => this.handleLetterChange.bind(this)}
        />
        <div className="accordion-wrap projects">
          <ProjectList />
        </div>
        <Pagination
          pages={this.state.pages}
          onClick={() => this.handleClick.bind(this)}
          onNext={() => this.handleNext.bind(this)}
        />
      </section>
    );
  }

  async handleChange(event) {
    debugger;
    const { name, value } = event.target;
    await this.setState({ [name]: value });
    this.loadProjects();
  }

  async handleClick(page) {
    debugger;
    await this.setState({ page: parseInt(page.target.innerText) });
    this.loadProjects();
  }

  async handleNext() {
    this.state.page < this.state.pages
      ? await this.setState((prevState) => {
          return { page: prevState.page + 1 };
        })
      : await this.setState((prevState) => {
          return { page: prevState.page };
        });
    this.loadProjects();
  }

  async handleLetterChange(letter) {
    debugger;
    await this.setState({ letter: letter.target.innerText });
    this.loadProjects();
  }

  async loadProjects() {
    const parameter = {
      name: this.state.name,
      page: this.state.page,
      letter: this.state.letter,
    };
    const data = await this.state.httpClient.getAllByPageAndQuery(parameter);
    if (data !== undefined) {
      this.setState({ projects: data.projects, pages: data.numberOfPages });
    }
  }
}

export default Projects;
