import React, { useState } from 'react';

interface InputFieldProps {
  label: string;
  initialValue: string;
  setValue: (value: string) => void;
  errorMessage?: string;
}

const InputField: React.FC<InputFieldProps> = ({ label, initialValue, setValue, errorMessage }) => {
  const [value, setLocalValue] = useState(initialValue);

  // Handle changes in the input field
  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const newValue = event.target.value;
    setLocalValue(newValue);
    setValue(newValue); // Pass the new value to the parent component
  };

  return (
    <div className="mb-4">
      {/* Input Label */}
      <label htmlFor={label} className="form-label fw-bold">
        {label}
      </label>

      {/* Input Field */}
      <input
        type="text"
        id={label}
        className={`form-control ${errorMessage ? 'is-invalid' : ''}`}
        value={value}
        onChange={handleChange}
        placeholder={`Enter your ${label.toLowerCase()}`}
      />

      {/* Error Message */}
      {errorMessage && <div className="invalid-feedback">{errorMessage}</div>}
    </div>
  );
};

export default InputField;
