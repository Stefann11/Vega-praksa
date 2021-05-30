import React from "react";
import { Route, Redirect } from "react-router-dom";

function AdminRoute({ component: Component, ...rest }) {
  debugger;
  const isAuthorized = checkIfValid();
  return (
    <Route
      {...rest}
      render={(props) => {
        if (isAuthorized) {
          return <Component />;
        } else {
          return (
            <Redirect to={{ pathname: "/", state: { from: props.location } }} />
          );
        }
      }}
    />
  );

  function checkIfValid() {
    const token =
      localStorage.getItem("token") || sessionStorage.getItem("token");
    const role =
      localStorage.getItem("role") === "Admin" ||
      sessionStorage.getItem("role") === "Admin";
    return token && role;
  }
}

export default AdminRoute;
