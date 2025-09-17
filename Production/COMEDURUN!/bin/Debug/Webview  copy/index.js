//Copyright (C) 2025 Lee Ki Joon
let accuracy = 90
let URL
let model
let webcam
let ctx
let currentPose
let maxPredictions
let port
let acc;
let socket;
const WS_URL = "ws://localhost:8080/pose";
let reconnectInterval = 2000; // Reconnect every 2 seconds

//Web socket connect function
function connect() {
    socket = new WebSocket(WS_URL);

    // When connection success
    socket.addEventListener('open', () => {
        console.log("Web socket");
    });

    // When message received
    socket.addEventListener('message', (event) => {
        console.log("Message received from server:", event.data);
    });

    // When connection finished(Retry connection)
    socket.addEventListener('close', () => {
        console.log("Web socket Connection finished");
        setTimeout(connect, reconnectInterval);
    });

    // When connection error(Retry connection)
    socket.addEventListener('error', (err) => {
        console.error("Web socket error:", err);
        socket.close();
    });
}

//Teachable Machine
async function startMachine() {
    const modelURL = "https://teachablemachine.withgoogle.com/models/OUSdwo86T/" + "model.json";
    const metadataURL = "https://teachablemachine.withgoogle.com/models/OUSdwo86T/" + "metadata.json";

    model = await tmPose.load(modelURL, metadataURL);
    maxPredictions = model.getTotalClasses();

    webcam = new tmPose.Webcam(600, 400, true);
    await webcam.setup();
    await webcam.play();
    window.requestAnimationFrame(loop);

    const canvas = document.getElementById("canvas");
    ctx = canvas.getContext("2d");
    currentPose = document.getElementById("current-pose");
    currentPose.appendChild(document.createElement("div"));
}

async function loop(_timestamp) {
    webcam.update();
    await predict();
    window.requestAnimationFrame(loop);
}

async function predict() {
    const { pose, posenetOutput } = await model.estimatePose(webcam.canvas);
    const prediction = await model.predict(posenetOutput);

    let max = 0, index = -1, maxStringPrediction = '';
    for (let i = 0; i < maxPredictions; i++) {
        const classPrediction = prediction[i].className + ": " + prediction[i].probability.toFixed(2);
        acc = Math.round(Number(prediction[i].probability.toFixed(2)) * 10000) / 100;
        if (acc > max) {
            max = acc;
            index = i;
            maxStringPrediction = classPrediction;
        }
    }

    if (max > accuracy && index >= 0) {
        // On screen
        currentPose.childNodes[0].innerHTML = maxStringPrediction;

        // Send current pose to web socket
        const poseName = prediction[index].className;
        if (socket && socket.readyState === WebSocket.OPEN) {
            socket.send(poseName);
            console.log("Send to server:", poseName);
        }
    }

    drawPose(pose);
}

function drawPose(pose) {
    if (webcam.canvas) {
        ctx.drawImage(webcam.canvas, 0, 0, canvas.width, canvas.height);

        if (pose) {
            const minimumAccuracy = 0.5;
            const scaleX = canvas.width / webcam.canvas.width;
            const scaleY = canvas.height / webcam.canvas.height;

            const scaledKeypoints = pose.keypoints.map(k => ({
                ...k,
                position: {
                    x: k.position.x * scaleX,
                    y: k.position.y * scaleY
                }
            }));

            tmPose.drawKeypoints(scaledKeypoints, minimumAccuracy, ctx);
            tmPose.drawSkeleton(scaledKeypoints, minimumAccuracy, ctx);
        }
    }
}

// Run!!!!!!!!!!!
connect();     // Try web socket connection
startMachine();