import './ErrorNotification.css'
import ErrorOutlineIcon from '@mui/icons-material/ErrorOutline';
const ErrorNotification = (props) => {

    return (
        <div className='container'>
            <div className='ErrorNotification'>
                <h1 className='ErrorTitle'>Oops! Something went wrong...</h1>
                <div className='ErrorText'><ErrorOutlineIcon fontSize='large'/> {props.message}</div>
                <div>
                    <img alt='error' src='https://res.cloudinary.com/kocewwcloud/image/upload/v1641636246/FishApp/ErrorImages/Error_tysbey.jpg' width={650} height='300px' />
                </div>
            </div>
        </div>
    )
}

export default ErrorNotification