import axios from "axios";

const BaseUrl: String = "https://localhost:7095";

async function GetActivities()  {
    axios.get(`${BaseUrl}/api/Activities`)
        .then(response => {
            console.log(response.data);
        }).catch(error => {
        console.log(error);
    })
}

export { GetActivities };