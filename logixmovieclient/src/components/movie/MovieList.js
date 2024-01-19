import { Stack } from '@mui/material'
import React from 'react'
import Movie from './Movie'
import { GetMovies, ReactionMovie } from '../../apis/MovieApi'
import { reactionType } from '../../commons/enums';
class MovieList extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            movies: [],
            loading: true,
        };
    }
    handleReact(movie, movieReactionType) {
        return ReactionMovie(movie.id, movieReactionType)
            .then(async response => {
                if (response && response.ok) {
                    return true
                }
                else return false
            })
    }
    componentDidMount() {
        GetMovies()
            .then(async (response) => {
                if (response && response.ok) {
                    var data = await response.json();
                    this.setState({ movies: data, loading: false })
                }
            })
            .catch((error) => {
                console.error('Error fetching movies:', error);
                this.setState({ loading: false });
            });
    }
    render() {
        const { movies, loading } = this.state;
        return (
            <div>
                <h2>Movies of today</h2>
                {
                    loading ? (
                        <p>Loading movies...</p>
                    ) : (
                        <Stack direction={'column'} gap={6}>
                            {
                                movies.map(x => (<Movie key={x.id} {...x} handleReact={this.handleReact}></Movie>))
                            }
                        </Stack>
                    )
                }
            </div >
        )
    }
}
export default MovieList