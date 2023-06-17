import express from 'express'
import cors from 'cors'
import bodyParser from 'body-parser'
import { v4 as uuidv4 } from 'uuid'

const appServer: express.Application = express()

appServer.use(cors())
appServer.use(bodyParser.json())

appServer.get("/", (req, res) => {
    res.send({ id: "OK" })
})

appServer.post("/login", (req, res) => {
    const username: string = req.body['username']
    res.send({ type: 0, secret: username })
})

appServer.listen(80)