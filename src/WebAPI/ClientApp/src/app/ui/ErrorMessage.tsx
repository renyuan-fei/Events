export function ErrorMessage({Error} : {Error: Error}) {
    return (
        <div>
            Some thing wrong
            {Error.message}
        </div>
    );
}