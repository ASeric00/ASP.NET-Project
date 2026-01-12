const BaseURL = 'http://127.0.0.1:5119'

class _WeatherStationAPI {

    // GET INFO FROM ALL STATIONS
    async GetAllStations() {
        const URL = `${BaseURL}/all`;
        const response = await fetch(URL, {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }
        });

        if(!response.ok) {
            console.error('Could not get all stations from the API!')
            return null;
        }

        return response.json();
    }

    // GET INFO FROM ONE STATION BY ITS NAME
    async GetOneStationByName(stationName) {
        const URL = `${BaseURL}/one/${stationName}`;
        const response = await fetch(URL, {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }
        });

        if(!response.ok) {
            console.error(`Could not get station ${stationName} from the API!`)
            return null;
        }

        return response.json();
    }

    // GET WEATHER RECORD BY ITS ID
    async GetRecordById(id) {
        const URL = `${BaseURL}/record/${id}`;
        const response = await fetch(URL, {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }
        });

        if(!response.ok) {
            console.error(`Could not get record #${id} from the API!`)
            return null;
        }

        return response.json();
    }

    //POST
    // Returns true if successful and false if failed
    async AddNewStation(newStation) {
        const URL = `${BaseURL}/new`;
        const response = await fetch(URL, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(newStation)
        });

        if(!response.ok) {
            console.error('Could not create new Weather Station info.')
            if(response.status === 400) { /* Bad Request */
                alert(await response.text())
            }
            return false;
        }

        return true;
    }

    //DELETE
    async DeleteAStation(id) {
        const URL = `${BaseURL}/delete/${id}`;
        const response = await fetch(URL, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json"
            }
        });

        if (!response.ok) {
            console.error(`Could not delete station with ID ${id} from the API!`);
            return null;
        }

        return response.json();
    }

    //UPDATE
    async UpdateAStation(id, newInfo) {
        const URL = `${BaseURL}/update/${id}`;
        const response = await fetch(URL, {
            method: "POST", // Use PUT for update operations (POST is usually for creating new data)
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(newInfo) // Pass the new station data as JSON
        });
    
        if (!response.ok) {
            console.error(`Could not update station with ID ${id} from the API!`);
            if (response.status === 400) {
                alert(await response.text()); // Show error if the server returns a bad request
            }
            return null;
        }
    
        return response.json(); // Return the updated station info if successful
    }

}

export const WeatherStationAPI = new _WeatherStationAPI();