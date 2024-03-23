const useSplitCamelCase = (input:string) => {
    return input.replace(/([A-Z])/g, ' $1').trim();
};

export default useSplitCamelCase;