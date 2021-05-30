import React, { Component } from "react";
import CreateNewProjectModal from "./CreateNewProjectModal";

class SearchElement extends Component {
  state = {
    showCreateNewProjectModal: false,
    createNew: false,
  };
  render() {
    return (
      <div className="wrapper">
        {this.state.showCreateNewProjectModal ? (
          <CreateNewProjectModal
            show={this.state.showCreateNewProjectModal}
            onShowChange={this.displayModalCreate.bind(this)}
          />
        ) : null}

        <div className="grey-box-wrap reports">
          <div style={{ float: "left" }} className="link new-member-popup">
            <a
              href="javascript:;"
              onClick={() => {
                this.displayModalCreate();
              }}
            >
              Create new project
            </a>
          </div>
          <div style={{ float: "right" }}>
            <input
              type="text"
              name="name"
              value={this.props.name}
              onChange={this.props.onChange}
              className="in-search"
            ></input>
          </div>
        </div>
      </div>
    );
  }

  displayModalCreate() {
    debugger;
    this.setState({
      showCreateNewProjectModal: !this.state.showCreateNewProjectModal,
    });
  }
}

export default SearchElement;
