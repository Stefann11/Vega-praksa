import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import DailyTimeSheetPage from "./pages/DailyTimeSheetPage";
import DayPage from "./pages/DayPage";
import ProjectPage from "./pages/ProjectPage";
import LoginPage from "./pages/LoginPage";
import ProtectedRoute from "./routes/ProtectedRoute";
import AdminRoute from "./routes/AdminRoute";
import ReportPage from "./pages/ReportPage";
import ResetPasswordPage from "./pages/ResetPasswordPage";

function App() {
  debugger;
  return (
    <Router>
      <div>
        <Switch>
          <AdminRoute path="/project" component={ProjectPage} />
          <ProtectedRoute path="/report" component={ReportPage} />
          <Route path="/day/:date" render={(props) => <DayPage {...props} />} />
          <Route path="/login">
            <LoginPage />
          </Route>
          <ProtectedRoute exact path="/reset" component={ResetPasswordPage} />
          <ProtectedRoute exact path="/" component={DailyTimeSheetPage} />
        </Switch>
      </div>
    </Router>
  );
}

export default App;
