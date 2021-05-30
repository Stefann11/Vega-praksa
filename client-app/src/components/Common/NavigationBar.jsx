import React, { Component } from "react";
import { NavLink } from "react-router-dom";

class NavigationBar extends Component {
  state = {
    loggedRole: "",
  };

  componentDidMount() {
    const role = sessionStorage.getItem("role") || localStorage.getItem("role");
    this.setState({
      loggedRole: role,
    });
  }

  render() {
    const AdminNavBar = () => {
      return (
        <ul className="menu">
          <li>
            <NavLink exact to="/" className="btn nav">
              TimeSheet
            </NavLink>
          </li>
          <li>
            <a href="" className="btn nav">
              Clients
            </a>
          </li>
          <li>
            <NavLink to="/project" className="btn nav">
              Projects
            </NavLink>
          </li>
          <li>
            <a href="" className="btn nav">
              Categories
            </a>
          </li>
          <li>
            <a href="" className="btn nav">
              Team members
            </a>
          </li>
          <li className="last">
            <NavLink to="/report" className="btn nav">
              Reports
            </NavLink>
          </li>
        </ul>
      );
    };

    const WorkerNavBar = () => {
      return (
        <ul className="menu">
          <li>
            <NavLink exact to="/" className="btn nav">
              TimeSheet
            </NavLink>
          </li>
          <li className="last">
            <NavLink to="/report" className="btn nav">
              Reports
            </NavLink>
          </li>
        </ul>
      );
    };

    const NavBar = () => {
      return this.state.loggedRole === "Admin" ? (
        <AdminNavBar />
      ) : (
        <WorkerNavBar />
      );
    };
    return (
      <nav>
        <NavBar />
        <div className="mobile-menu">
          <a href="javascript:;" className="menu-btn">
            <i className="zmdi zmdi-menu"></i>
          </a>
          <ul>
            <li>
              <a href="javascript:;">TimeSheet</a>
            </li>
            <li>
              <a href="javascript:;">Clients</a>
            </li>
            <li>
              <a href="javascript:;">Projects</a>
            </li>
            <li>
              <a href="javascript:;">Categories</a>
            </li>
            <li>
              <a href="javascript:;">Team members</a>
            </li>
            <li className="last">
              <a href="javascript:;">Reports</a>
            </li>
          </ul>
        </div>
        <span className="line"></span>
      </nav>
    );
  }
}

export default NavigationBar;
