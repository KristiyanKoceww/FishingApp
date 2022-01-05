import React, { useState, useEffect, useContext } from 'react';
import { Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import { withRouter } from 'react-router-dom';
import { useHistory } from 'react-router-dom';
import { UserContext } from "../UserContext";

const IdleMonitor = () => {
    const [idleModal, setIdleModal] = useState(false);
    const { appUser, setAppUser } = useContext(UserContext);
    const refreshTokenUrl = process.env.REACT_APP_REFRESHTOKEN;

    let idleTimeout = 15 * 60 * 1000;  
    let idleLogout = 16 * 60 * 1000;
    let idleEvent;
    let idleLogoutEvent;

    const jwt = localStorage.getItem("jwt");
    const refresh = localStorage.getItem("refresh");
    let history = useHistory();
    const events = [
        'mousemove',
        'click',
        'keypress'
    ];

    //This function is called with each event listener to set a timeout or clear a timeout.
    const sessionTimeout = () => {
        if (!!idleEvent) clearTimeout(idleEvent);
        if (!!idleLogoutEvent) clearTimeout(idleLogoutEvent);

        idleEvent = setTimeout(() => setIdleModal(true), idleTimeout); //show session warning modal.
        idleLogoutEvent = setTimeout(() => logOut(), idleLogout); //Call logged out on session expire.
    };

    const extendSession = () => {
        const data = {
            accessToken: jwt,
            refreshToken: refresh,
        }
        fetch(refreshTokenUrl, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        })
            .then((response) => response.json())
            .then((res) => {
                if (res.accessToken && res.refreshToken) {
                    localStorage.setItem("jwt", res.accessToken);
                    localStorage.setItem("refresh", res.refreshToken);
                    setIdleModal(false);
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }

    const logOut = () => {
        localStorage.removeItem("jwt");
        localStorage.removeItem("refresh");
        localStorage.removeItem("userId");
        setAppUser(null);
        setIdleModal(false);
        history.push("/");
    }

    useEffect(() => {
        for (let e in events) {
            window.addEventListener(events[e], sessionTimeout);
        }

        return () => {
            for (let e in events) {
                window.removeEventListener(events[e], sessionTimeout);
            }
        }
    }, []);


    return (

        <Modal isOpen={idleModal} toggle={() => setIdleModal(false)}>
            <ModalHeader>
                Session expire warning
            </ModalHeader>
            <ModalBody>
                your session will expire in {idleLogout / 60 / 1000} minutes. Do you want to extend the session?
            </ModalBody>
            <ModalFooter>
                <button className="btn btn-warning" onClick={() => logOut()}>Logout</button>
                <button className="btn btn-success" onClick={() => extendSession()}>Extend session</button>
            </ModalFooter>
        </Modal>
    )


}
export default withRouter(IdleMonitor);