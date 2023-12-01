import axios from "axios";

const BaseUrl: String = "https://localhost:7095";

async function GetAllActivities()  {
    axios.get(`${BaseUrl}/api/Activities`)
        .then(response => {
            console.log(response.data);
        }).catch(error => {
        console.log(error);
    })
}

export { GetAllActivities };