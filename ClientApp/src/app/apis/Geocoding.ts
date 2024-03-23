import apiClient from "@apis/BaseApi.ts";

const API_KEY = "e8a9569fb5b64faca1f9c3fb58bcee61"

const getCityName = async (latitude: number, longitude: number) => {
    const url = `https://api.opencagedata.com/geocode/v1/json?q=${latitude}+${longitude}&key=${API_KEY}`;

    const response = await apiClient.get(url);
    return response.data.results[0]?.components.city || 'Unknown city';
};

export default getCityName;