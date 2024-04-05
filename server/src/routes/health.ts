import { FastifyInstance } from "fastify"
import { ZodTypeProvider } from "fastify-type-provider-zod"

export async function health(app: FastifyInstance) {
  app
    .withTypeProvider<ZodTypeProvider>()
    .get("/healthz", async (request, reply) => {
      return reply.status(200).send("OK!")
    })
}
