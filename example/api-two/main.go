package main

import (
	"fmt"
	"net/http"

	spinhttp "github.com/fermyon/spin/sdk/go/v2/http"
	"github.com/fermyon/spin/sdk/go/v2/kv"
)

func init() {
	spinhttp.Handle(func(w http.ResponseWriter, r *http.Request) {
		s, err := kv.OpenStore("default")
		if err != nil {
			http.Error(w, "Error talking to Redis", 500)
			return
		}
		s.Set("SampleKey", []byte("Foobar"))
		w.Header().Set("Content-Type", "text/plain")
		fmt.Fprintln(w, "Hello from API 2!")
	})
}

func main() {}
