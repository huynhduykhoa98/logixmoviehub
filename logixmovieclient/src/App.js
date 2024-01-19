import React, { useState, useEffect } from 'react'
import { makeStyles } from '@mui/styles'
import { AppBar, Toolbar, Typography, Container, Paper, Stack, IconButton, Tooltip } from '@mui/material'
import { localStorageEnum } from './commons/enums'
import LogoutIcon from '@mui/icons-material/Logout'
import MovieList from './components/movie/MovieList'
import Login from './components/auth/Login'
import { GetInfoApi } from './apis/UserApi'

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  appBar: {
    position: 'fixed !important',
    zIndex: 1,
    top: '0px',
    marginBottom: theme.spacing(2),
  },
  container: {
    marginTop: '64px',
    paddingBottom: theme.spacing(4),
    paddingTop: theme.spacing(4),
  },
  content: {
    padding: theme.spacing(2),
  },
  toolBar: {
    display: 'flex',
    justifyContent: 'space-between'
  },
}))

const App = () => {
  const classes = useStyles()
  const [isLoggedIn, setLoggedIn] = useState(() => {
    return JSON.parse(localStorage.getItem(localStorageEnum.isLoggedIn) ?? false)
  })
  useEffect(() => {
    var access_token = localStorage.getItem(localStorageEnum.access_token);
    if (access_token) {
      GetInfoApi().then(async (response) => {
        if (response && response.ok) {
          var info = await response.json()
          localStorage.setItem(localStorageEnum.name, info.email);
          setLoggedIn(true)
        } else {
          setLoggedIn(false)
        }
      })
    }
  }, [])
  const name = localStorage.getItem(localStorageEnum.name)
  const handleLogout = () => {
    setLoggedIn(false)
    localStorage.removeItem(localStorageEnum.token_type)
    localStorage.removeItem(localStorageEnum.access_token)
  }
  return (
    <div className={classes.root}>
      {isLoggedIn ? (
        <>
          <AppBar position="static" className={classes.appBar}>
            <Container maxWidth="xl">
              <Toolbar className={classes.toolBar}>
                <Typography variant="h6">
                  LOGIX MOVIE HUB
                </Typography>
                {isLoggedIn && (
                  <Stack
                    alignItems={'center'}
                    justifyContent={'space-between'}
                    direction={{ xs: 'row' }}
                    spacing={{ xs: 2 }}>
                    <b>
                      {name}
                    </b>
                    <Tooltip title="Logout">
                      <IconButton color="inherit" onClick={handleLogout}>
                        <LogoutIcon></LogoutIcon>
                      </IconButton>
                    </Tooltip>
                  </Stack >
                )}
              </Toolbar>
            </Container>
          </AppBar>
          <Container className={classes.container}>
            <Stack direction={'column'}>
              <Paper className={classes.content}>
                <Typography variant="h5">
                  Hi {name}, Welcome to movie hub
                </Typography>
              </Paper>
              <MovieList></MovieList>
            </Stack>
          </Container>
        </>
      ) : (
        <Login ></Login>
      )}
    </div>

  )
}

export default App
