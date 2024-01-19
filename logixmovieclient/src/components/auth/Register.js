import "../../commons/style.css";
import React from 'react'
import TextField from '@mui/material/TextField'
import Button from '@mui/material/Button'
import { Container, Typography, Grid, Stack, FormControl } from '@mui/material'
import theme from '../theme'
import { Link } from 'react-router-dom'
import { RegisterApi } from "../../apis/UserApi";

class Register extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      email: '',
      password: '',
      confirmPassword: '',
      error: ''
    };
  }

  handleRegister = async (e) => {
    e.preventDefault();
    if (this.state.email.trim() !== '' && this.state.password.trim() !== '' && this.state.confirmPassword.trim() !== '') {
      if (this.state.password.trim() != this.state.confirmPassword.trim()) {
        alert('Password do not match')
      }
      else {
        var response = await RegisterApi(this.state.email, this.state.password, this.state.confirmPassword)
        if (response && response.ok)
          window.location.href = '/login'
        else {
          var res = await response.json()
          if (res.errors) {
            alert(res.errors[0])
          } else if (res.message) {
            alert(res.message)
          }
        }
      }
    } else {
      alert('Please enter info')
    }
  }

  render() {
    return (
      <div >
        <Container maxWidth='lg' style={{ height: '100vh', justifyContent: 'center', display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
          <Typography variant="h4" marginBottom={theme.spacing(4)}>
            Welcome to LOGIX MOVIE HUB
          </Typography>
          <form className='form' style={{ width: '100%' }} onSubmit={this.handleRegister}>
            <strong>Register new account</strong>
            <Grid className='grid-row' container xs={12} spacing={theme.spacing(2)}>
              <Grid item xs={4}>
                <strong>Email</strong>
              </Grid>
              <Grid item xs={8}>
                <TextField
                  required={true}
                  variant="outlined"
                  margin="normal"
                  fullWidth
                  type="email"
                  value={this.state.email}
                  onChange={(e) => this.setState({ email: e.target.value })}
                />
              </Grid>
            </Grid>
            <Grid className='grid-row' container xs={12} spacing={theme.spacing(2)}>
              <Grid item xs={4}>
                <strong>Password</strong>
              </Grid>
              <Grid item xs={8}>
                <TextField
                  type="password"
                  required={true}
                  variant="outlined"
                  margin="normal"
                  fullWidth
                  value={this.state.password}
                  onChange={(e) => this.setState({ password: e.target.value })}
                />
              </Grid>
            </Grid>
            <Grid className='grid-row' container xs={12} spacing={theme.spacing(2)}>
              <Grid item xs={4}>
                <strong>Confirm Password</strong>
              </Grid>
              <Grid item xs={8}>
                <TextField
                  type="password"
                  variant="outlined"
                  margin="normal"
                  fullWidth
                  value={this.state.confirmPassword}
                  onChange={(e) => this.setState({ confirmPassword: e.target.value })}
                />
              </Grid>
            </Grid>
            <Grid className='grid-row' container xs={12} spacing={theme.spacing(2)}>
              <Grid item xs={12}>
                <Stack direction={'row'} justifyContent={'space-between'}>
                  <Link color='success' to={'/login'}>Already has account</Link>
                  <Button variant="contained" color="primary" type="submit">
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

export default Register