import React from "react";
import DashboardSidebar from "./dashboardsidebar";

const Dashboard: React.FC = () => {
  return (
    <div className="dashboard-container">
      <DashboardSidebar /> 
      <div className="main-content">
        <h1>Admin Dashboard</h1>
        <p>Hier kun je je shows beheren, enzovoort...</p>
      </div>
    </div>
  );
};

export default Dashboard;