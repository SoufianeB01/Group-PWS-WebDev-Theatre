import React, { ChangeEventHandler, useEffect, useState } from "react";
import { propTypes } from "react-bootstrap/esm/Image";
import {fetchAllShows, TheaterShow,TheaterShowFormProps,ShowEntry,Show,registration} from "./EditshowState";
import { Header } from "../Header/header";
export type RegistrationState = Show & {
    updateTitle: (
      Title: string
    ) => (state: RegistrationState) => RegistrationState;
    updateDescription: (
      Description: string
    ) => (state: RegistrationState) => RegistrationState;
    updatePrice: (
      Price: number
    ) => (state: RegistrationState) => RegistrationState;
    UpdateVenueId:(
    VenueId: number
    ) => (state: RegistrationState) => RegistrationState;
  };
  
  export const initRegistrationState: RegistrationState = {
    Title: "",
    Description: "",
    Price: 18,
    VenueId: 1,
    // storage: Map(),
    // currentId: 0,
    updateTitle:
      (Title: string) =>
      (state: RegistrationState): RegistrationState => ({
        ...state,
        Title: Title,
      }),
    updateDescription:
      (Description: string) =>
      (state: RegistrationState): RegistrationState => ({
        ...state,
        Description: Description,
      }),
    updatePrice:
      (Price: number) =>
      (state: RegistrationState): RegistrationState => ({
        ...state,
        Price: Price,
      }),
    UpdateVenueId:
      (VenueId: number) =>
      (state: RegistrationState): RegistrationState => ({
        ...state,
        VenueId: VenueId,
      }),
  };
  
export interface RegistrationProps {
    insertShow: (_: Show) => TheaterShow;
    
    registrationState: RegistrationState;
  }
  
  export class RegistrationForm extends React.Component<
    RegistrationProps,
    RegistrationState
  > {
    constructor(props: RegistrationProps) {
      super(props);
      this.state = { ...props.registrationState }; 
    }
  
    handleInputChange =
      (field: keyof TheaterShow) =>
      (event: React.ChangeEvent<HTMLInputElement>) => {
        const value =
          field === "Price"
            ? event.currentTarget.valueAsNumber
            : event.currentTarget.value;
  
        this.setState((prevState) => ({
          ...prevState,
          [field]: value,
        }));
      };
  
    render() {
      const { Title, Description, Price,VenueId=1} = this.state;
  
      return (
        <div>
          <div key="registration-form-Title">
            First Title:
            <input value={Title} onChange={this.handleInputChange("Title")} />
          </div>
          <div key="registration-form-last-Title">
            Description:
            <input
              value={Description}
              onChange={this.handleInputChange("Description")}
            />
          </div>
          <div key="registration-form-Price">
            Price:
            <input
              value={Price.valueOf()}
              type="Number"
              onChange={this.handleInputChange("Price")}
            />
          </div>
          <div>
            <button
              onClick={async (_) =>
              { await registration(
                this.props.insertShow({
                  Title: this.state.Title,
                  Description: this.state.Description,
                  Price: this.state.Price,
                  VenueId: this.state.VenueId
                }))
            }
            }
              
            >
              Submit
            </button>
          </div>
        </div>
      );
    }
  }

  export default RegistrationForm;