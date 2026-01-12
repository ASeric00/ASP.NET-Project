import { WeatherStationAPI } from './weatherStationAPI.js'; // Assuming your API class is in this file
import { GetConditionIcon } from './get-condition-icons.js';

let editingStationId = null; // Track which station is being edited

document.getElementById("save-record-button").addEventListener("click", async () => {
    const stationName = document.getElementById("stationName").value;
    const conditions = document.getElementById("conditions").value;
    const temperature = document.getElementById("temperature").value;
    const pressure = document.getElementById("pressure").value;
    const windSpeed = document.getElementById("windSpeed").value;
    const windDirection = document.getElementById("windDirection").value;

    const newStation = {
        stationName,
        conditions,
        temperature,
        pressure,
        windSpeed,
        windDirection,
    };

    if (editingStationId) {
        // If editing, update the station
        const success = await WeatherStationAPI.UpdateAStation(editingStationId, newStation);
        if (success) {
            alert('Station updated successfully!');
            clearFields();  // Clear the form fields
            document.getElementById("save-record-button").textContent = "Save"; // Reset button text
            editingStationId = null;  // Reset editing state
        }
    } else {
        // If not editing, add a new station
        const success = await WeatherStationAPI.AddNewStation(newStation);
        if (success) {
            alert('New station added successfully!');
            clearFields();  // Clear the form fields
        }
    }
});

document.getElementById("clear-all-info-button").addEventListener("click", clearFields);

// Showing past records from database
document.getElementById("show-records-button").addEventListener("click", async () => {
    const stations = await WeatherStationAPI.GetAllStations();
    const tableBody = document.getElementById("past-records-table-body");
    tableBody.innerHTML = '';  // Clear the table body

    stations.forEach(station => {
        const row = document.createElement('tr');

        row.innerHTML = `
            <td>${station.stationName}</td>
            <td>${GetConditionIcon(station.conditions)}</td>
            <td>${station.temperature}</td>
            <td>${station.pressure}</td>
            <td>${station.windSpeed}</td>
            <td>${station.windDirection}</td>
            <td>
                <button class="edit-btn" data-id="${station.id}">Edit</button>
                <button class="delete-btn" data-id="${station.id}">Delete</button>
            </td>
        `;

        row.querySelector(".edit-btn").addEventListener("click", () => editStation(station.id));
        row.querySelector(".delete-btn").addEventListener("click", () => deleteStation(station.id));

        tableBody.appendChild(row);
    });

    document.getElementById("past-records-table").style.display = "table"; // Show the table
});

// Clear form fields
function clearFields() {
    document.getElementById("stationName").value = '';
    document.getElementById("conditions").value = 'sunny';
    document.getElementById("temperature").value = '';
    document.getElementById("pressure").value = '';
    document.getElementById("windSpeed").value = '';
    document.getElementById("windDirection").value = '';
}

// Edit station (triggered when "Edit" is clicked)
async function editStation(id) {
    const station = await WeatherStationAPI.GetRecordById(id);
    document.getElementById("stationName").value = station.stationName;
    document.getElementById("conditions").value = station.conditions;
    document.getElementById("temperature").value = station.temperature;
    document.getElementById("pressure").value = station.pressure;
    document.getElementById("windSpeed").value = station.windSpeed;
    document.getElementById("windDirection").value = station.windDirection;

    // Change the button functionality to "Update" after filling the form
    document.getElementById("save-record-button").textContent = "Update";
    editingStationId = id; // Track the station being edited
}

// Delete station (triggered when "Delete" is clicked)
async function deleteStation(id) {
    const success = await WeatherStationAPI.DeleteAStation(id);
    if (success) {
        alert('Station deleted successfully!');
        document.getElementById("show-records-button").click(); // Refresh the table
    }
}