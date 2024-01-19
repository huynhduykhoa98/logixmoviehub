import React from 'react'
import "../../commons/style.css";
import TextField from '@mui/material/TextField'
import Button from '@mui/material/Button'
import { Container, Typography, Grid, Stack } from '@mui/material'
import theme from '../theme'
import { Link } from 'react-router-dom'
import { localStorageEnum } from '../../commons/enums';
import { LoginApi } from '../../apis/UserApi'
class Login extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            email: '',
            password: '',
        };
    }
    handleLogin = async () => {
        if (this.state.email.trim() !== '' && this.state.password.trim() !== '') {
            try {
                var response = await LoginApi(this.state.email.trim(), this.state.password.trim())
                if (response && response.ok) {
                    var res = await response.json()
                    localStorage.setItem(localStorageEnum.access_token, res.access_token)
                    localStorage.setItem(localStorageEnum.token_type, res.token_type)
                    window.location.href = '/'
                } else {
                    if (response) {
                        var res = await response.json()
                        if (res.errors) {
                            alert(res.errors[0])
                        } else if (res.message) {
                            alert(res.message)
                        }
                    }
                }
            } catch (ex) {
                alert('Login fail')
                console.log(ex)
            }
        } else {
            alert('Please enter both email and password.')
        }
    }
    render() {
        return (
            <div >
                <Container maxWidth='lg' style={{ height: '100vh', justifyContent: 'center', display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                    <Typography variant="h4" marginBottom={theme.spacing(4)}>
                        Welcome to LOGIX MOVIE HUB
                    </Typography>
                    <form className='form' style={{ width: '100%' }}>
                        <Grid className='grid-row' container spacing={theme.spacing(2)}>
                            <Grid item xs={4}>
                                <strong>Email</strong>
                            </Grid>
                            <Grid item xs={8}>
                                <TextField
                                    variant="outlined"
                                    margin="normal"
                                    fullWidth
                                    type='email'
                                    value={this.state.email}
                                    onChange={(e) => this.setState({ email: e.target.value })}
                                />
                            </Grid>
                        </Grid>
                        <Grid className='grid-row' container spacing={theme.spacing(2)}>
                            <Grid item xs={4}>
                                <strong>Password</strong>
                            </Grid>
                            <Grid item xs={8}>
                                <TextField
                                    type="password"
                                    variant="outlined"
                                    margin="normal"
                                    fullWidth
                                    value={this.state.password}
                                    onChange={(e) => this.setState({ password: e.target.value })}
                                />
                            </Grid>
                        </Grid>
                        <Grid className='grid-row' container spacing={theme.spacing(2)}>
                            <Grid item xs={12}>
                                <Stack direction={'row'} justifyContent={'space-between'}>
                                    <Link color='success' to={'/register'}>Register</Link>
                                    <Button variant="contained" color="primary" onClick={this.handleLogin}>
                                        Submit
                                    </Button>
                                </Stack>
                            </Grid>
                        </Grid>
                    </form>
                </Container>
            </div >
        )
    }
}
export default Login