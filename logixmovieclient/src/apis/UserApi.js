import { localStorageEnum } from "../commons/enums"

const API_BASE_URL = process.env.REACT_APP_API_BASE_URL
const API_INFO_URL = 'api/user/me'
const API_LOGIN_URL = 'api/user/login'
const API_REGISTER_URL = 'api/user/register'


export const GetInfoApi = async () => {
    try {
        const tokenType = localStorage.getItem(localStorageEnum.token_type) ?? ''
        const accessToken = localStorage.getItem(localStorageEnum.access_token) ?? ''
        var respone = await fetch(`${API_BASE_URL}/${API_INFO_URL}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `${tokenType} ${accessToken}`
            }
        });
        return respone;
    } catch (error) {
        return null;
    }
}
export const LoginApi = async (email, password) => {
    try {
        var respone = await fetch(`${API_BASE_URL}/${API_LOGIN_URL}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ email, password }),
        })
        return respone;
    } catch (error) {
        return {
            success: false,
            message: error.message
        };
    }
};
export const RegisterApi = async (email, password, confirmPassword) => {
    try {
        var respone = await fetch(`${API_BASE_URL}/${API_REGISTER_URL}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ email, password, confirmPassword }),
        });
        return respone;
    } catch (error) {
        return null;
    }
}; 