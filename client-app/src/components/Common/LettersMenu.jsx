import React, { Component } from "react";

class LettersMenu extends Component {
  state = {};
  render() {
    const letters = [
      "a",
      "b",
      "c",
      "d",
      "e",
      "f",
      "g",
      "h",
      "i",
      "j",
      "k",
      "l",
      "m",
      "n",
      "o",
      "v",
      "p",
      "q",
      "r",
      "s",
      "t",
      "u",
      "w",
      "x",
      "y",
      "z",
    ];
    const Letters = () =>
      letters.map((letter) => (
        <li>
          <a href="javascript:;" onClick={this.props.onClick(letter)}>
            {letter}
          </a>
        </li>
      ));
    return (
      <div className="alpha">
        <ul>
          <Letters />
        </ul>
      </div>
    );
  }
}

export default LettersMenu;
