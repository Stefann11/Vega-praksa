import React, { Component } from "react";

class Pagination extends Component {
  state = {
    pages: this.props.pages,
  };

  render() {
    const numbers = Array.from({ length: this.props.pages }, (x, i) => (
      <li>
        <a href="javascript:;" onClick={this.props.onClick(i + 1)}>
          {" "}
          {i + 1}
        </a>
      </li>
    ));
    return (
      <div className="pagination">
        <ul>
          {numbers}
          <li className="last">
            <a href="javascript:;" onClick={this.props.onNext()}>
              Next
            </a>
          </li>
        </ul>
      </div>
    );
  }
}

export default Pagination;
