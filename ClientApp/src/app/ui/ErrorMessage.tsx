const ErrorMessage = ({Error}: { Error: Error }) => {
    return (
        <div>
            Some thing wrong
            {Error.message}
        </div>
    );
}

export default ErrorMessage;