import { localStorageEnum } from "../commons/enums";

const API_BASE_URL = process.env.REACT_APP_API_BASE_URL
const API_MOVIE_LIST = 'api/movie/list'
const API_MOVIE_REACTION = 'api/movie/reaction'

export const GetMovies = async () => {
    try {
        var respone = await fetch(`${API_BASE_URL}/${API_MOVIE_LIST}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `${localStorage.getItem(localStorageEnum.token_type) ?? ''} ${localStorage.getItem(localStorageEnum.access_token) ?? ''}`
            }
        });
        return respone;
    } catch (error) {
        return null;
    }
}
export const ReactionMovie = async (movieId, movieReactionType) => {
    try {
        var respone = await fetch(`${API_BASE_URL}/${API_MOVIE_REACTION}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `${localStorage.getItem(localStorageEnum.token_type) ?? ''} ${localStorage.getItem(localStorageEnum.access_token) ?? ''}`
            }, body: JSON.stringify({ movieId, movieReactionType }),
        });
        return respone;
    } catch (error) {
        return null;
    }
}