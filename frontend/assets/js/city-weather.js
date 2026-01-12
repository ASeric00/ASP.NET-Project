const baseURL = 'http://127.0.0.1:5119'; // Base URL of your API

import { GetConditionIcon } from "/assets/js/get-condition-icons.js"

// Function to fetch weather data for a specific station
async function fetchStationData(stationName) {
    const response = await fetch(`${baseURL}/one/${stationName}`);
    if (!response.ok) {
        console.error(`Failed to fetch data for station: ${stationName}`);
        return null;
    }
    return response.json();
}

// Function to render weather data on the page
function renderWeatherData(stationName, records) {
    const weatherDataDiv = document.getElementById('weather-data');
    weatherDataDiv.innerHTML = ''; // Clear any existing content

    if (!records || records.length === 0) {
        weatherDataDiv.innerHTML = `<h2>No data available for ${stationName}</h2>`;
        return;
    }

    // Ensure records are sorted by timestamp in ascending order
    records.sort((a, b) => new Date(b.timestamp).getTime() - new Date(a.timestamp).getTime());

    // Get the latest record (first after sorting)
    const latestRecord = records[0];

    // Display the latest record separately
    const latestHTML = `
        <h2>Latest Weather Record for ${stationName}</h2>
        <p><strong>Conditions:</strong> ${GetConditionIcon(latestRecord.conditions)}</p>
        <p><strong>Temperature:</strong> ${latestRecord.temperature}°C</p>
        <p><strong>Pressure:</strong> ${latestRecord.pressure} hPa</p>
        <p><strong>Wind Speed:</strong> ${latestRecord.windSpeed} km/h</p>
        <p><strong>Wind Direction:</strong> ${latestRecord.windDirection}</p>
        <p><strong>Timestamp:</strong> ${formatTimestampToCustomFormat(latestRecord.timestamp)}</p>
    `;

    // Generate table excluding the latest record
    const previousRecords = records.slice(1); // Skip the first record

    const tableRows = previousRecords
        .map(record => `
            <tr>
                <td>${GetConditionIcon(record.conditions)}</td>
                <td>${record.temperature}</td>
                <td>${record.pressure}</td>
                <td>${record.windSpeed}</td>
                <td>${record.windDirection}</td>
                <td>${formatTimestampToCustomFormat(record.timestamp)}</td>
            </tr>
        `)
        .join('');

    const tableHTML = previousRecords.length > 0
        ? `
        <h2>All Previous Records</h2>
        <table border="1">
            <thead>
                <tr>
                    <th>Conditions</th>
                    <th>Temperature (°C)</th>
                    <th>Pressure (hPa)</th>
                    <th>Wind Speed (km/h)</th>
                    <th>Wind Direction</th>
                    <th>Timestamp</th>
                </tr>
            </thead>
            <tbody>
                ${tableRows}
            </tbody>
        </table>
    `
        : '<h3>No previous records available</h3>'; // If no previous records exist

    // Combine latest record and table
    weatherDataDiv.innerHTML = latestHTML + tableHTML;
}

function formatTimestampToCustomFormat(timestamp) {
    // Extract year, month, day, hours, and minutes from the ISO format string
    const [datePart, timePart] = timestamp.split('T');
    const [year, month, day] = datePart.split('-');
    const [hours, minutes] = timePart.split(':');

    // Return formatted string
    return `${day}.${month}.${year} ${hours}:${minutes}`;
}

// Event listener for submenu buttons
document.querySelectorAll('.city-button').forEach(button => {
    button.addEventListener('click', async () => {
        const stationName = button.getAttribute('data-city');
        const records = await fetchStationData(stationName);
        renderWeatherData(stationName, records);
    });
});
