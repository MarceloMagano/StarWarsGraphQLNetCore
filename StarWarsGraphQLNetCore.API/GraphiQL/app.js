import React from 'react'
import ReactDOM from 'react-dom'
import GraphiQL from 'graphiql'
import axios from 'axios'
import 'graphiql/graphiql.css'
import './app.css'

function graphQLFetcher(graphQLParams) {
  return axios({
    method: 'POST',
    url: window.location.origin + '/graphql',
    data: graphQLParams
  }).then(resp => resp.data)

  // return fetch(window.location.origin + '/graphql', {
  //   method: 'post',
  //   headers: { 'Content-Type': 'application/json' },
  //   body: JSON.stringify(graphQLParams)
  // }).then(response => response.json())
}

ReactDOM.render(<GraphiQL fetcher={graphQLFetcher}/>, document.getElementById('app'))
