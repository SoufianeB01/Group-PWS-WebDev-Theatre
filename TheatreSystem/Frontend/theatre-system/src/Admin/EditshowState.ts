import React, { ChangeEventHandler, useEffect, useState } from "react";
import { propTypes } from "react-bootstrap/esm/Image";



export type TheaterShow = {
    TheaterShowID: number;
    Title: string;
    Description: string;
    Price: number;
    VenueID: number;
  };

export type TheaterShowFormProps = {
    initialData?: TheaterShow;
    onSubmit: (data: TheaterShow) => void;
  };

export const fetchAllShows = async () => {
    try {
      const response = await fetch("api/[controller]"); 
      if (!response.ok) {
        throw new Error('Failed to fetch shows');
      }
      return await response.json(); 
    } catch (error) {
      console.error('Error fetching shows:', error);
      throw error;
    }
  };
  export interface Show {
    Title: string;
    Description: string;
    Price: Number;
    VenueId: number;
  }
  export type ViewState = "home" | "registration" | "overview";
  

export type ShowEntry = Show & { id: number };
export const Delete = async (reservationId:number) => 
    {
        if (reservationId === null) {
            alert("Please enter a reservation ID.");
            return;
          }
        
          try {
            const response = await fetch(`/api/Reservations/Delete?id=${reservationId}`, {
              method: "DELETE",
            });
          }
          catch (error) {
            console.error("Error deleting show:", error);
            alert("Failed to delete the show.");
            
          }
    
    }

export const registration = async (newShow:TheaterShow) => 
    {
        try {
            const response = await fetch("/api/TheaterShows/CreateShow", {
              method: "POST",
              headers: {
                "Content-Type": "application/json",
              },
              body: JSON.stringify(newShow),
            });
        
            if (!response.ok) {
              throw new Error("Failed to create show");
            }
        
            const data = await response.json();
            console.log("Show created:", data);
            alert("Show created successfully!");
          } catch (error) {
            console.error("Error creating show:", error);
            alert("Failed to create the show.");
          }
    
    }