import axios from "axios";
import {Activity} from "@models/Activity.ts";

const BaseUrl: String = "https://localhost:7095";

export async function GetActivities({page = 1, searchTerm = []}: {
    page: number,
    searchTerm: string[]
} = {page: 1, searchTerm: []}) {
    const params = new URLSearchParams();

    searchTerm.forEach(term => params.append("SearchTerm", term));

    let url = "";

    if (params.size > 0) {
        url = `${BaseUrl}/api/Activities/${page}/?${params.toString()}`
    } else {
        url = `${BaseUrl}/api/Activities/${page}`
    }


    try {
        const response = await axios.get<Activity[]>(url);
        return response.data;

    } catch (error) {
        // 更好的错误处理
        console.error(error);
        throw error;
    }
}


export async function GetActivity(id: string) {
    axios.get(`${BaseUrl}/api/Activities/${id}`)
        .then(response => {
            console.log(response.data);
        }).catch(error => {
        console.log(error);
    })
}