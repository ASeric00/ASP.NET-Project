import { WeatherStationAPI } from "/assets/js/weatherStationAPI.js";
import { GetConditionIcon } from "/assets/js/get-condition-icons.js"

window.onload = (e) => {
    // Immediately update the table on site load
    UpdateWeatherStationsTable();

    // Automatically update the table at regular intervals (e.g., every 10 seconds)
    setInterval(UpdateWeatherStationsTable, 10000); // 10,000 ms = 10 seconds
};

async function UpdateWeatherStationsTable() {
    const weatherStationList = await WeatherStationAPI.GetAllStations();
    if (!weatherStationList) {
        console.error('Could not load weather stations info.');
        return;
    }

    const table = document.getElementById('all-cities-table');
    if (!table) {
        console.error('Could not find weather stations table.');
        return;
    }

    // Create a map to store the latest record for each city
    const latestMeasurements = {};

    weatherStationList.forEach(station => {
        const cityName = station.stationName.trim(); // Ensure consistent city name
        const currentTimestamp = station.timestamp; 
    
        // Check if this city is already in the map or if the new timestamp is more recent
        if (
            !latestMeasurements[cityName] || 
            currentTimestamp > latestMeasurements[cityName].timestamp
        ) {
            latestMeasurements[cityName] = station; // Store or update with the latest record
        }
    
        // Format the timestamp to dd.MM.yyyy HH:mm
        // const formattedTimestamp = formatTimestampToCustomFormat(currentTimestamp);
    
        // console.log(`Processing ${cityName}: ${formattedTimestamp}`); // Log formatted timestamp
    
    });
    
    // Prepare table data with only the latest measurements
    let data = `
    <thead>
        <tr>
            <th>City</th>
            <th>Conditions</th>
            <th>Temperature</th>
            <th>Pressure</th>
            <th>Wind speed</th>
            <th>Wind direction</th>
            <th>Timestamp</th>
        </tr>
    </thead>
    `;

    data += '<tbody>';
    Object.values(latestMeasurements).forEach(station => {
        data += `
            <tr>
                <td>${station.stationName}</td>
                <td>${GetConditionIcon(station.conditions)}</td>
                <td>${station.temperature}</td>
                <td>${station.pressure}</td>
                <td>${station.windSpeed}</td>
                <td>${station.windDirection}</td>
                <td>${formatTimestampToCustomFormat(station.timestamp)}</td>

            </tr>
        `;
    });
    data += '</tbody>';

    // Update the table's innerHTML
    table.innerHTML = data;
}

function formatTimestampToCustomFormat(timestamp) {
    // Extract year, month, day, hours, and minutes from the ISO format string
    const [datePart, timePart] = timestamp.split('T');
    const [year, month, day] = datePart.split('-');
    const [hours, minutes] = timePart.split(':');

    // Return formatted string
    return `${day}.${month}.${year} ${hours}:${minutes}`;
}
